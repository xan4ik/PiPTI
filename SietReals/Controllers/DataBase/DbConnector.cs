using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SietReals.Controllers
{
    public class ImageTuple
    {
        public int Id { get; set; }
        public string text { get; set; }
        public string imageName { get; set; }
        public string messaga { get; set; }
    }

    public class DbConnector : DbContext
    {
        public DbSet<ImageTuple> HelpArTuples { get; set; }
        public DbSet<ImageTuple> HelpSoftwareTuples { get; set; }
        public DbSet<ImageTuple> TutorialLevelTuples { get; set; }
        public DbSet<ImageTuple> TutorialDifficultTuples { get; set; }
        public DbSet<ImageTuple> TutorialRuleTuples { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=imageInfo; Trusted_Connection=True");
        }

        public void Init() // тестовые данные
        {
            HelpArTuples.AddRange(
                    new ImageTuple()
                    {
                        Id = 1,
                        imageName = "ar.jpg",
                        text = "AR"
                    },
                    new ImageTuple()
                    {
                        Id = 2,
                        imageName = "def.jpg",
                        text = "AR"
                    }
                );
            HelpSoftwareTuples.AddRange(
                    new ImageTuple()
                    {
                        Id = 1,
                        imageName = "piano.jpg",
                        text = "Software"
                    },
                    new ImageTuple()
                    {
                        Id = 2,
                        imageName = "def.jpg",
                        text = "Software"
                    }
                );
            TutorialLevelTuples.AddRange(
                    new ImageTuple()
                    {
                        Id = 1,
                        imageName = "piano.jpg",
                        text = "Level"
                    }
                );
            TutorialRuleTuples.AddRange(
                    new ImageTuple()
                    {
                        Id = 1,
                        imageName = "ar.jpg",
                        text = "Rule"
                    },
                    new ImageTuple()
                    {
                        Id = 2,
                        imageName = "piano.jpg",
                        text = "Rule"
                    }
                );
            TutorialDifficultTuples.AddRange(
                    new ImageTuple()
                    {
                        Id = 1,
                        imageName = "ar.jpg",
                        text = "Difficult"
                    }
                );

            SaveChanges();
        }
    }
}
