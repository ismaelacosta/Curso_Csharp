using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.Data.Entities;

namespace Shopping.Helpers
{
    public interface IComboHelper
    {
         Task<IEnumerable<SelectListItem>> GetComboCategoriasAsync();
        Task<IEnumerable<SelectListItem>> GetComboCategoriasAsync(IEnumerable<Category> filter);


        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();

         Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);

         Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);

    }
}
