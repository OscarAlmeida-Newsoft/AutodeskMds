using DTOs;

namespace IServices
{    
    public interface IVariableService : IBaseService<VariableDTO>
    {
        /// <summary>
        /// Actualiza una variable de la tabla de variables
        /// </summary>
        /// <param name="pVariableDTO">Id de la variable que se actualizará</param>
        int AddAndReturnID(VariableDTO pVariableDTO);

        /// <summary>
        /// Actualiza una variable de la tabla de variables
        /// </summary>
        /// <param name="pVariableDTO">Id de la variable que se actualizará</param>
        void UpdateVariable(VariableDTO pVariableDTO);
    }
}
