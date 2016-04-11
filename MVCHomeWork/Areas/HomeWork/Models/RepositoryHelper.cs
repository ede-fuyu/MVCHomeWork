namespace MVCHomeWork.Areas.HomeWork.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static BankInfoRepository GetBankInfoRepository()
		{
			var repository = new BankInfoRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static BankInfoRepository GetBankInfoRepository(IUnitOfWork unitOfWork)
		{
			var repository = new BankInfoRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static BaseUserRepository GetBaseUserRepository()
		{
			var repository = new BaseUserRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static BaseUserRepository GetBaseUserRepository(IUnitOfWork unitOfWork)
		{
			var repository = new BaseUserRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CompanyRepository GetCompanyRepository()
		{
			var repository = new CompanyRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CompanyRepository GetCompanyRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CompanyRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ConfigCodeRepository GetConfigCodeRepository()
		{
			var repository = new ConfigCodeRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ConfigCodeRepository GetConfigCodeRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ConfigCodeRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ContactsRepository GetContactsRepository()
		{
			var repository = new ContactsRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ContactsRepository GetContactsRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ContactsRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CustomersListRepository GetCustomersListRepository()
		{
			var repository = new CustomersListRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CustomersListRepository GetCustomersListRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CustomersListRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}