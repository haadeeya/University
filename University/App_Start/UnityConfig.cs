using Core.Registration;
using Core.StudentManager;
using Core.SubjectManager;
using Interface;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace University
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterFactory<SqlConnection>(c =>
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
            );

            container.RegisterType<ISubjectDAL, SubjectDAL>();
            container.RegisterType<IStudentDAL, StudentDAL>();
            container.RegisterType<IUserDAL, UserDAL>();

            container.RegisterType<ISubjectBL, SubjectBL>();
            container.RegisterType<IStudentBL, StudentBL>();
            container.RegisterType<IUserBL, UserBL>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}