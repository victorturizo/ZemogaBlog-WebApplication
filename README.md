# ZemogaBlog-WebApplication
Front-End to ZemogaBlog

This Project was Created using razor pages, to run this project be sure that the api it's running in the following port  https://localhost:44327/

Every pages is located in page folder, but the initial page it's located on the root pages folder

- We have 3 types of users: Deault User(Guess), Admin(Vturizo) and Writer(Writer123), all of them harcoded on index page on the root of page folder

            List<User> usersDummy = new List<User>();
            usersDummy.Add(new User()
            {
                Id = 1,
                Email = "VictorTurizo91@hotmail.com",
                Password = "12345",
                Profile = "Admin",
                Username = "vhturizo"
            });
            usersDummy.Add(new User()
            {
                Id = 2,
                Email = "Writer123@hotmail.com",
                Password = "12345",
                Profile = "Writer",
                Username = "Writer123"
            }            
            );
            
  - All the validations was performed using session objects, when you get authenticated with you prefered rol, you can check what type of actions you can perform as was requested     in the test
  
  -time spent developing this website and functionality : 10 hours
