//using System.Data.Entity;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace MiyGarden.Models.Entities
//{
//	public class User : IdentityUser<string, UserLogin, UserRole, UserClaim>
//	{
//		public async Task<ClaimsIdentity>
//			GenerateUserIdentityAsync(ApplicationUserManager manager)
//		{
//			var userIdentity = await manager
//				.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//			return userIdentity;
//		}

//		[Index]
//		[Display(Name = "姓名")]
//		[MaxLength(50), Column(Order = 0), Required]
//		public string LoginName { get; set; }
//    }
//}