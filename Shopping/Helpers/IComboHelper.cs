using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopping.Helpers
{
    public interface IComboHelper
    {
         Task<IEnumerable<SelectListItem>> GetComboCategoriasAsync();

         Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();

         Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);

         Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);

    }
}
