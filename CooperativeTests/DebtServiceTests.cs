using System;
using Cooperative.Data;
using Cooperative.Models;   
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CooperativeTests
{
    public class DebtServiceTests
    {
        private Cooperator CreateTestCooperator(CooperatorStatus status = CooperatorStatus.Active, DateTime startDate = default)
        {
            return new Cooperator
            {
                Name = "Test User",
                Status = status,
                CoopNumber = "123",
                StaffNumber = "12345",
                Factory = "Yale 5",
                Department = "Manufacturing",
                PostHeld = "Parker",
                MembershipCommencementDate = startDate,
                DateOfEmployment = DateTime.Now.AddYears(-2),
                IsMemberOfSimilarSociety = false,
                MaritalStatus = "Single",
                MonthlyContribution = 100,
                BuildingFundBalance = 0,
                InvestmentBalance = 0,
                SharesBalance = 0,
                LoanBalance = 0,
                FoodBalance = 0,
                SouvenirBalance = 0,
                OtherBalance = 0

            };
        }
        private CooperativeDbContext CreateDbContext()
        {
            var dbName = $"CoopTest_{Guid.NewGuid()}";

            var options = new DbContextOptionsBuilder<CooperativeDbContext>()
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={dbName};Trusted_Connection=True;")
                .Options;

            var context = new CooperativeDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task TakeLoan_IsSuccessIsFalse_WhenCooperator_NotFound()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);

            //Act
            var loan = await service.TakeLoan(10000, 23, TransactionType.BankTransfer);

            //Assert
            Assert.False(loan.IsSuccess);
        }

        [Fact]
        public async Task TakeLoan_IsSuccessIsFalse_WhenCooperator_NotActive()
        {
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(status: CooperatorStatus.Inactive);

            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var loan = await service.TakeLoan(10000, cooperator.Id, TransactionType.BankTransfer);

            //Assert
            Assert.False(loan.IsSuccess);
        }

        [Fact]
        public async Task TakeLoan_IsSuccessIsFalse_WhenCommencementDateIsLessThanSixMonths()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(startDate: DateTime.Now.AddMonths(-3));

            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var loan = await service.TakeLoan(10000, cooperator.Id, TransactionType.BankTransfer);


            //Assert
            Assert.False(loan.IsSuccess);
        }

        [Fact]
        public async Task TakeLoan_IsSuccessIsFalse_WhenThereIsAnActiveLoan()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(startDate: DateTime.Now.AddMonths(-8));
            context.Add(cooperator);
            await context.SaveChangesAsync();

            var loan = new Loan
            {
                CooperatorId = cooperator.Id,
                PrincipalAmount = 10000,
                TotalRepayable = 11000,
                MonthlyInstallment = 1000,
                DateTaken = DateTime.Now.AddMonths(-1),
                LoanTransactionType = TransactionType.BankTransfer
            };
            context.Add(loan);
            await context.SaveChangesAsync();

            //Act
            var takeLoan = await service.TakeLoan(10000, cooperator.Id, TransactionType.BankTransfer);

            //Assert
            Assert.False(takeLoan.IsSuccess);
        }

        [Fact]
        public async Task TakeLoan_IsSuccessIsTrue_WhenAllConditionsAreMet()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(startDate: DateTime.Now.AddMonths(-8));
            context.Add(cooperator);
            await context.SaveChangesAsync();

            var loan = await service.TakeLoan(100000, cooperator.Id, TransactionType.BankTransfer);

            await context.Entry(cooperator).ReloadAsync();
            var loanRecord = await context.Loans.FirstOrDefaultAsync(l => l.CooperatorId == cooperator.Id);

            Assert.True(loan.IsSuccess);
            Assert.NotNull(loanRecord);
            Assert.Equal(-110000, cooperator.LoanBalance);
        }

        [Fact]
        public async Task TakeFood_IsSuccessIsFalse_WhenCooperator_NotFound()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);

            //Act
            var food = await service.TakeFood(27000, 23, 3, "Dec 25 Food", 75764);

            //Assert
            Assert.False(food.IsSuccess);
            Assert.Equal("Cooperator not found.", food.Message);
        }

        [Fact]
        public async Task TakeFood_IsSuccessIsFalse_WhenCooperator_NotActive()
        {
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(status: CooperatorStatus.Pending);

            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var food = await service.TakeFood(27000, cooperator.Id, 3, "Dec 25 Food", 75764);

            //Assert
            Assert.False(food.IsSuccess);
            Assert.Equal("Only active cooperators can take food.", food.Message);
        }

        [Fact]
        public async Task TakeFood_IsSuccessIsTrue_WhenAllConditionsAreMet()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(startDate: DateTime.Now.AddMonths(-8));
            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var food = await service.TakeFood(27000, cooperator.Id, 3, "Dec 25 Food", 75764);
            await context.Entry(cooperator).ReloadAsync();
            var foodRecord = await context.Food.FirstOrDefaultAsync(f => f.CooperatorId == cooperator.Id);
            //Assert
            Assert.True(food.IsSuccess);
            Assert.NotNull(foodRecord);
            Assert.Equal(-27000, cooperator.FoodBalance);
        }

        [Fact]
        public async Task TakeSouvenir_IsSuccessIsFalse_WhenCooperator_NotFound()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);

            //Act
            var souvenir = await service.TakeSouvenir(20000, 23, "AGM 25 Souvenir", 3);

            //Assert
            Assert.False(souvenir.IsSuccess);
            Assert.Equal("Cooperator not found.", souvenir.Message);
        }


        [Fact]
        public async Task TakeSouvenir_IsSuccessIsFalse_WhenCooperator_NotActive()
        {
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(status: CooperatorStatus.Pending);

            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var souvenir = await service.TakeSouvenir(20000, cooperator.Id, "AGM 25 Souvenir", 3);

            //Assert
            Assert.False(souvenir.IsSuccess);
            Assert.Equal("Only active cooperators can take souvenir.", souvenir.Message);
        }

        [Fact]
        public async Task TakeSouvenir_IsSuccessIsTrue_WhenAllConditionsAreMet()
        {
            //Arrange
            var context = CreateDbContext();
            var service = new Cooperative.Services.DebtService(context);
            var cooperator = CreateTestCooperator(startDate: DateTime.Now.AddMonths(-8));
            context.Add(cooperator);
            await context.SaveChangesAsync();

            //Act
            var souvenir = await service.TakeSouvenir(20000, cooperator.Id, "AGM 25 Souvenir", 2);
            await context.Entry(cooperator).ReloadAsync();
            var souvenirRecord = await context.Souvenirs.FirstOrDefaultAsync(s => s.CooperatorId == cooperator.Id);

            //Assert
            Assert.True(souvenir.IsSuccess);
            Assert.NotNull(souvenirRecord);
            Assert.Equal(-20000, cooperator.SouvenirBalance);
        }
    }
}
