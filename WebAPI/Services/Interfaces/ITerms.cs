using WebAPI.Models.Entities;

namespace WebAPI.Services.Interfaces
{
    public interface ITerms
    {
        List<ExamTerm> GetAllTerms();
        ExamTerm GetTerm(ExamTerm examTerm);


    }
}
