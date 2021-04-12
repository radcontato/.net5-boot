using Dapper;
using DevCars.Entities;
using DevCars.InputModels;
using DevCars.Persistence;
using DevCars.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DevCars.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }


        // GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            //var cars = _dbContext.Cars;

            //com EF Core

            //var carsViewModel = cars
            //    .Where(c => c.Status == CarStatusEnum.Available)
            //    .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
            //    .ToList();


            //com Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Brand, Model, Price FROM tb_car WHERE Status = 0";

                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

                return Ok(carsViewModel);
            }


        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var carsDetailViewModel = new CarDetailsViewModel(
                car.Id,
                car.Brand,
                car.Model,
                car.VinCode,
                car.Year,
                car.Price,
                car.Color,
                car.ProductionDate
                );

            return Ok(carsDetailViewModel);
        }

        // POST api/cars

        /// <summary>
        /// Cadastrar um Carro
        /// </summary>
        /// <remarks>
        /// Requisicao de exemplo
        /// {
        ///     "brand": "Honda",
        ///     "model": "Civic",
        ///     "vinCode": "abc123",
        ///     "year": "2021",
        ///     "color": "Cinza",
        ///     "productionDate": "2021-04-05"   
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo Carro</param>
        /// <returns>Objeto recem-criado</returns>
        /// <response code="201"> Objeto criado com sucesso.</response>
        /// <response code="400"> Dados Invalidos</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo nao pode ter mais de 50 caractesres");
            }


            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = car.Id },
                model
                );
        }

        // PUT api/cars/1
        /// <summary>
        /// Atualizar dados de um Carro
        /// </summary>
        /// <remarks>
        /// Requisicao de exemplo:
        /// {
        ///     "color": "Vermelho",
        ///     "Price": "100000"
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um Carro</param>
        /// <param name="model">Dados de alteracao</param>
        /// <returns>bNao tem retorno</returns>
        /// <response code="204">Atualizacao bem-sucedida</response>
        /// <response code="400">Dados invalidos</response>
        /// <response code="404">Carros nao encontrado</response>


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var car = _dbContext.Cars
                .SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound(0);
            }

            car.Update(model.Color, model.Price);

            //com Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE tb_car set Color = @color, Price = @price WHERE Id = @id";

                sqlConnection.Execute(query, new { color = car.Color, price = car.Price, id = car.Id });
            }

            _dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();

            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
