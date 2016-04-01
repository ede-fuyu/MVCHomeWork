using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class ConfigCodeRepository : EFRepository<ConfigCode>, IConfigCodeRepository
	{

	}

	public  interface IConfigCodeRepository : IRepository<ConfigCode>
	{

	}
}