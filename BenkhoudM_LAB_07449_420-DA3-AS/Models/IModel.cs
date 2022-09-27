using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Models
{
    public interface IModel<TModel> where TModel : IModel<TModel>
    {
        /// <summary>
        /// Inserts the values of a <see cref="IModel{TModel}"/> instance in the database as a
        /// new entry.
        /// </summary>
        /// <returns>The instance with the updated database identity property set.</returns>
        void Insert();
        /// <summary>
        /// Populates an instance of <see cref="IModel{TModel}"/> with property values retrieved from
        /// a database entry based on its identity identifier.
        /// </summary>
        /// <returns>The instance with updated property values from the database.</returns>
        void GetById();
        /// <summary>
        /// Updates the database entry with values from the instance's properties.
        /// </summary>
        /// <returns>The instance</returns>
        void Update();
        /// <summary>
        /// Deletes the database entry.
        /// </summary>
        void Delete();
    }
}

