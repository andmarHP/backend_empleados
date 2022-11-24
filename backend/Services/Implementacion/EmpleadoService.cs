using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Services.Contrato;

namespace backend.Services.Implementacion
{
    public class EmpleadoService : IEmpleadoService
    {
        private DbempleadoContext _dbContext;

        public EmpleadoService( DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Empleado>> GetList()
        {
            try
            {
                List<Empleado> lista = new List<Empleado>();

                lista = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentoNavigation).ToListAsync();
                
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> Get(int IdEmpleado)
        {
            try
            {
                Empleado? encontrado = new Empleado();

                encontrado = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentoNavigation)
                       .Where(e => e.IdEmpleado == IdEmpleado).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> Add(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Add(modelo);
                await _dbContext.SaveChangesAsync();

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Task<bool> Update(Empleado modelo)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Delete(Empleado modelo)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
