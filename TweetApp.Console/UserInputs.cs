using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Service.ModelDto;

namespace TweetApp.console
{
    public class UserInputs
    {
        public RegisterModelDto EnterResgisterModel()
        {
            RegisterModelDto newUser = new RegisterModelDto();
            Console.WriteLine("Register");

            Console.WriteLine("Enter Firstname");
            newUser.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Lastname");
            newUser.LastName = Console.ReadLine();
            Console.WriteLine("Enter Gender");
            newUser.Gender = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth(yyyy/mm/dd)");
            string userCreated = Console.ReadLine();
            string[] values = userCreated.Split("/");
            //validate Date
            newUser.DOB = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));

            Console.WriteLine("Phone");
            newUser.Phone = Console.ReadLine();

            Console.WriteLine("Email Address");
            newUser.Email = Console.ReadLine();

            Console.WriteLine("Password");
            newUser.Password = Console.ReadLine();
            return newUser;
        }
        public LoginModelDto EnterLoginModel()
        {
            LoginModelDto newUser = new LoginModelDto();
           
            Console.WriteLine("Email Address");
            newUser.Email = Console.ReadLine();

            Console.WriteLine("Password");
            newUser.Password = Console.ReadLine();
            return newUser;
        }
        public ResetPasswordModelDto EnterResetPasswordModel()
        {
            ResetPasswordModelDto model = new ResetPasswordModelDto();

            Console.WriteLine("Email Address");
            model.Email = Console.ReadLine();
            Console.WriteLine("Old Password");
            model.OldPassword = Console.ReadLine();

            Console.WriteLine("New Password");
            model.NewPassword = Console.ReadLine();
            return model;
        }
        public ForgetPasswordModelDto EnterForgetPasswordModel()
        {
            ForgetPasswordModelDto model = new ForgetPasswordModelDto();

            Console.WriteLine("Email Address");
            model.Email = Console.ReadLine();          
            Console.WriteLine("Enter Date of Birth(yyyy/mm/dd)");
            string userCreated = Console.ReadLine();
            string[] values = userCreated.Split("/");
            //validate Date
            model.DOB = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));

            Console.WriteLine("New Password");
            model.NewPassword = Console.ReadLine();
            return model;
        }
        public TweetDto EnterTweetDto()
        {
            TweetDto model = new TweetDto();

           
            Console.WriteLine("Message");
            model.Message = Console.ReadLine();
            model.AddedDate = DateTime.Now;
            return model;
        }
    }
}
