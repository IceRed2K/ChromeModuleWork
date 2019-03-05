# ChromeModuleWork
Read Cookies / Passwords using .dll

            
            Work manage = new Work();
            
            //Get Passwords
            var passwords = manage.GetPasswords("Path to Login Data file");
            
            //Output Passwords
            foreach (var item in passwords)
                Console.WriteLine("{0}  |  {1} : {2}", item.Item1, item.Item2, item.Item3);
                
                
            //Get Cookies
            var cookies = manage.GetCookies("Path to Login Data file")
            
             //Output Cookies  
            foreach (var item in cookies)
                Console.WriteLine("{0}  |  {1} : {2}", item.Item1, item.Item2, item.Item3);
