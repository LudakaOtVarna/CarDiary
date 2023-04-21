using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarDiary.Data.Models;
using CarDiary.Services.Mapping;

namespace CarDiary.Web.ViewModels.Administration.Users
{
	public class UserModel : IMapFrom<ApplicationUser>
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
