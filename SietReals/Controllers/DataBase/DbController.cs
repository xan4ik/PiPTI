using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SietReals.Controllers
{
    public class DbController : Controller
    {
        public void ChangeContext(string contexName) 
        {
            DbService.Service().ChangeContexTo(contexName);
        }

        public IActionResult GetNextImage()
        {
            var imageTuple = DbService.Service().GetNextImage();
            return Json(imageTuple);
        }

        public IActionResult GetPrevImage()
        {
            var imageTuple = DbService.Service().GetPrevImage();
            return Json(imageTuple);
        }
    }

    public interface IDbContext
    { 
        ImageTuple GetNext();
        ImageTuple GetPrev();
        bool IsContextName(string contextName);
        void Reset();
    }
    public class DbServiceContext : IDbContext
    {
        private ImageTuple[] tuples;  
        private string name;
        private int index;

        public DbServiceContext(string name, IEnumerable<ImageTuple> tuples)
        {
            this.name = name;
            this.tuples = tuples.ToArray();
            this.index = -1;
        }

        public void Reset() 
        {
            index = -1;
        }

        public bool IsContextName(string contextName)
        {
            return name == contextName;
        }

        public ImageTuple GetNext() 
        {
            index = Math.Min(index + 1, tuples.Length - 1);
            return tuples[index];
        }
        public ImageTuple GetPrev() 
        {
            index = Math.Max(index - 1, 0);
            return tuples[index];
        }
    }
    public class DbService 
    {
        private static DbService instance;
        private IDbContext[] contexts;
        private IDbContext current;
        private DbConnector connector;
        private DbService()
        {
            connector = new DbConnector();
            contexts = new IDbContext[]
            {
                new DbServiceContext("AR", connector.HelpArTuples),
                new DbServiceContext("Soft", connector.HelpSoftwareTuples),
                new DbServiceContext("Level", connector.TutorialLevelTuples),
                new DbServiceContext("Rules", connector.TutorialRuleTuples),
                new DbServiceContext("Difficult", connector.TutorialDifficultTuples)
            };
            current = contexts[0];
        }

        static DbService() 
        {
            instance = new DbService();
        }

        public void ChangeContexTo(string contexName) 
        {
            foreach (var contex in contexts)
            {
                if (contex.IsContextName(contexName)) 
                {
                    current = contex;
                    return;
                }
            }
        }

        public ImageTuple GetNextImage() 
        {
            return current.GetNext();
        }
        public ImageTuple GetPrevImage() 
        {
            return current.GetPrev();
        }

        public static DbService Service()
        {
            return instance;
        }
    }
}
