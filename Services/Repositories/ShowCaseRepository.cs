using MoveNowB.Data;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Repositories
{
    public class ShowCaseRepository:IShowCaseRepository
    {
        private readonly AppDbContext _context;
        public ShowCaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public ShowCase AddShow(ShowCase showCase)
        {
            _context.ShowCases.Add(showCase);
            _context.SaveChanges();
            return showCase;
        }

        public ShowCase DeleteShow(int id)
        {
            ShowCase showCase = _context.ShowCases.Find(id);
            if (showCase != null)
            {
                _context.ShowCases.Remove(showCase);
                _context.SaveChanges();
            }
            return showCase;
        }

        public IEnumerable<ShowCase> GetShowCases()
        {
            return _context.ShowCases.Where(x => x.ShowType == ShowType.Advert);
        }
        public IEnumerable<ShowCase> GetHideShowCases()
        {
            return _context.ShowCases.Where(x => x.ShowType == ShowType.Normal);
        }
        public ShowCase GetShowCaseById(int id)
        {
            return _context.ShowCases.Find(id);
        }

        public ShowCase UpdateShow(ShowCase updatedShow)
        {
            var show = _context.ShowCases.Attach(updatedShow);
            show.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedShow;
        }
    }
}
