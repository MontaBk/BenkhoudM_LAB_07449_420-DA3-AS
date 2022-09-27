using BenkhoudM_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Views
{
    internal interface IModelView<TModel> : IView where TModel : IModel<TModel>
    {
        void Render(TModel modelInstance);
    }
}
