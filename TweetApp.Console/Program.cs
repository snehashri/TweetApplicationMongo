using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TweetApp.Domain.DBContext;
using TweetApp.Domain.Repository;
using TweetApp.Service.Services;

namespace TweetApp.console
{
   static class Program
    {
        private static string tokenimp;
        public static void WaitForResult(string s)
        {
            Console.WriteLine(s);
            Console.ReadLine();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            #region Configuration
            var serviceCollection = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json").Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);        
            
            serviceCollection.AddDbContext<TweetAppDatabaseSettings>(opts => opts.UseSqlServer(configuration.GetConnectionString("TweetAppDB"))); 
            
            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            serviceCollection.AddScoped<IUserRegistrationService, UserRegistrationService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ITweetRepository, TweetRepository>();
            serviceCollection.AddScoped<ITweetService, TweetService>();
            serviceCollection.AddSingleton<TweetApp>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var tweetApp = serviceProvider.GetService<TweetApp>();

            #endregion

            UserInputs In = new UserInputs();
            bool isLogin = false;

            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            
            while (true)
            {
                while (!isLogin)
                {
                  
                    Console.WriteLine("My Tweet App! \n");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Forgot Password");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine("Press any key to close the application");
                    Console.WriteLine("\nSelect ");
                    int selectedOption = Int32.Parse(Console.ReadLine());
                    
                    switch (selectedOption)
                    {

                        case 1:
                            var user = In.EnterResgisterModel();
                            var res = tweetApp.UserRegistration(user);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + res.Message + "\n");
                            Console.ResetColor();
                            WaitForResult("Enter any key to go back to the menu");
                            break;
                        case 2:
                            var loginuser = In.EnterLoginModel();
                            var loginRes = tweetApp.UserLogin(loginuser);
                            if (loginRes.StatusCode == 200)
                            {
                                isLogin = true;
                                tokenimp = loginRes.Data;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(loginRes.Message);
                                Console.ResetColor();
                                WaitForResult("Enter any key to go in Dashboard");

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n" + loginRes.Message + "\n");
                                Console.ResetColor();
                                WaitForResult("Enter any key");
                            }

                            break;

                        case 3:
                            var forgetpass = In.EnterForgetPasswordModel();
                            var ForPassRes = tweetApp.ForgotPasword(forgetpass);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + ForPassRes.Message + "\n");
                            Console.ResetColor();
                            WaitForResult("Enter any key to go in Dashboard");
                            break;

                        case 4:
                            Console.WriteLine("Exit");
                            Environment.Exit(0);

                            break;
                        default:
                            Console.WriteLine("Select options from above");
                            WaitForResult("Enter any key to go back to the menu");

                            break;

                    }
                }
                while (isLogin)
                {
                    Console.WriteLine("My Tweet App! \n");
                    Console.WriteLine("1. Post a Tweet");
                    Console.WriteLine("2. View my Tweets");
                    Console.WriteLine("3. View all Tweets");
                    Console.WriteLine("4. View all Users");
                    Console.WriteLine("5. Reset Password");
                    Console.WriteLine("6. Logout\n");

                    Console.WriteLine("Select");
                    int userSelectedOption = Int32.Parse(Console.ReadLine());
                    switch (userSelectedOption)
                    {
                        case 1:
                            int userid = obj.GetUserId(tokenimp);
                            string emailid = obj.GetEmail(tokenimp);
                            var tmodel = In.EnterTweetDto();
                            tmodel.UserId = userid;
                            tmodel.Email = emailid;
                            var posttweetsRes = tweetApp.PostTweet(tmodel);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + posttweetsRes.Message + "\n");
                            Console.ResetColor();
                            WaitForResult("Enter any key to go back to the menu");
                            break;

                        case 2:
                            int userid1 = obj.GetUserId(tokenimp);
                            var viewmytweet = tweetApp.ViewAllTweetsOfUser(userid1);
                            foreach (var tweet in viewmytweet.Data)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(tweet.Email);
                                Console.WriteLine(" Tweet : " + tweet.Message + " " + tweet.AddedDate + "\n");
                                Console.ResetColor();
                            }
                            WaitForResult("Enter any key to go back to the menu");
                            break;
                        case 3:
                            var tweetsRes = tweetApp.ViewAllTweets();
                            foreach (var tweet in tweetsRes.Data)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(tweet.Email);
                                Console.WriteLine(" Tweet : " + tweet.Message + " " + tweet.AddedDate + "\n");
                                Console.ResetColor();
                            }
                            WaitForResult("Enter any key to go back to the menu");
                            break;

                        case 4:
                            var usersRes = tweetApp.ViewAllUsers();
                            foreach (var u in usersRes.Data)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(" User : " + u.FirstName);
                                Console.WriteLine(" Email : " + u.Email + "\n");
                                Console.ResetColor();
                            }
                            WaitForResult("Enter any key to go back to the menu");
                            break;
                        case 5:
                            var resetPassModel = In.EnterResetPasswordModel();
                            var res = tweetApp.ResetPassword(resetPassModel);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + res.Message + "\n");
                            Console.ResetColor();
                            WaitForResult("Enter any key to go back to the menu");
                            break;
                        case 6:
                            int userid3 = obj.GetUserId(tokenimp);
                            var logoutRes = tweetApp.LogOut(userid3);
                            tokenimp = null;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + logoutRes.Message + "\n");
                            Console.ResetColor();
                            isLogin = false;
                            WaitForResult("Enter any key to go back to the menu");
                            break;

                        default:
                            Console.WriteLine("Select any option");
                            break;
                    }
                }
            }


        }
    }
}
