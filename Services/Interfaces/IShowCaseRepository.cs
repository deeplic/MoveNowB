using MoveNowB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Interfaces
{
    public interface IShowCaseRepository
    {
        ShowCase AddShow(ShowCase showCase);
        ShowCase GetShowCaseById(int id);
        IEnumerable<ShowCase> GetHideShowCases();
        IEnumerable<ShowCase> GetShowCases();
        ShowCase UpdateShow(ShowCase updatedShow);
        ShowCase DeleteShow(int id);
    }
}
