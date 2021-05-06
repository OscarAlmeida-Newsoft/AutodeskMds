namespace IServices
{
    using DTOs;
    using System.Collections.Generic;

    public interface IProductService : IBaseService<ProductDTO>
    {
        /// <summary>
        /// Actualiza una fila de la tabla de productos
        /// </summary>
        /// <param name="pProductDTO">Id del producto</param>
        void UpdateProduct(ProductDTO pProductDTO);

        IEnumerable<ProductDTO> GetByFamilyID(int id);
    }
}
