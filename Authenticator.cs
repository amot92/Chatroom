using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Authenticator
{
    public List<String> users = new List<String>();
    public String path;

    public Authenticator(String path)
    {
        this.path = path;
        getUsers();//pull whole list into program at runtime
    }

    public void getUsers()
    {
        while (true)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    String userString = sr.ReadLine();
                    users.Add(userString);
                }
                sr.Close();
                break;
            }
            catch (FileNotFoundException nfe)
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Close();
                Console.WriteLine("File not found exception : {0}", nfe.ToString());
                //exception still crashing program
            }
            catch (EndOfStreamException ese)
            {
                Console.WriteLine("End of Stream exception : {0}", ese.ToString());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception : {0}", ex.ToString());
                break;
            }
        }
    }

    //takes <username>,<password> string
    public bool authenticate(String request)
    {
        foreach(String user in users)
        {
            if (user.Equals(request))
            {
                return true;
            }
        }
        return false;
    }

    public bool check(String userString)
    {
        foreach(String user in users)
        {
            String[] strs = user.Split(',');
            String userName = strs[0];
            strs = userString.Split(',');
            String name = strs[0];
            if (userName.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
    
    //takes <username>,<password> string
    public void register(String newUser)
    {
        users.Add(newUser);
        save();
    }

    public void save()
    {
        StreamWriter sw = new StreamWriter(path);

        foreach (String user in users)
        {
            sw.WriteLine(user);
        }
        sw.Close();
    }
}
