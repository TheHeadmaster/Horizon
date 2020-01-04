using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    /// <summary>
    /// Allows implementation of conversion between json data models and object models. Place on the
    /// json side.
    /// </summary>
    /// <typeparam name="TModel">
    /// The object model that this json model is associated with.
    /// </typeparam>
    public interface IFileToModel<TModel> where TModel : IModelToFile
    {
        /// <summary>
        /// Creates and returns an object model populated from json data.
        /// </summary>
        /// <returns>
        /// A new object model instance.
        /// </returns>
        TModel CreateModel();

        /// <summary>
        /// Populates this json data model with object data.
        /// </summary>
        /// <param name="model">
        /// The object model to populate from.
        /// </param>
        void PopulateFile(TModel model);
    }
}