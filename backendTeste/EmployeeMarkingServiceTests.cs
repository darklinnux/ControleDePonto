using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using backend.Exceptions;

namespace backendTeste
{
    public class EmployeeMarkingServiceTests
    {
        private Mock<IEmployeeMarkingRepository> employeeMarkingRepositoryMock;
        private Mock<IEmployeeService> employeServiceMock;
        private Mock<IMarkingService> markingServiceMock;
        private Mock<IMapper> mapperMock;
        private EmployeeMarkingService employeeMarkingService;

        public EmployeeMarkingServiceTests()
        {
            employeeMarkingRepositoryMock = new Mock<IEmployeeMarkingRepository>();
            employeServiceMock = new Mock<IEmployeeService>();
            markingServiceMock = new Mock<IMarkingService>();
            mapperMock = new Mock<IMapper>();

            employeeMarkingService = new EmployeeMarkingService(
                employeeMarkingRepositoryMock.Object,
                mapperMock.Object,
                employeServiceMock.Object,
                markingServiceMock.Object
            );
        }

        [Fact]
        public async Task BatidaPontoValidaQuandoCadastraBatidaPontoEntaoRegistroDeveSerEfetuadoComSucesso()
        {
            // Arrange
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 2,
                DateTime = DateTime.ParseExact("10/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Act
            var result = await employeeMarkingService.Add(employeeCreate);

            // Assert
            AssertRecordWasCreated(result, employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoForaJornadaQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 2,
                DateTime = DateTime.ParseExact("10/05/2024 18:10", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Configurar o comportamento do serviço de marcação para lançar uma exceção
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("A marcação deve está de acordo com sua escala de trabalho"));

            // Act e Assert
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoDiaForaEscalaQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 1,
                DateTime = DateTime.ParseExact("30/03/2024 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Configurar o comportamento do serviço de marcação para lançar uma exceção
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("Dia da semana não condiz com a sua escala"));

            // Act e Assert
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoSaidaSemEntradaQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 2,
                DateTime = DateTime.ParseExact("17/05/2024 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Configurar o comportamento do serviço de marcação para lançar uma exceção
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("Só pode haver uma saída se tiver uma entrada"));

            // Act e Assert
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoSegundaEntradaQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 1,
                DateTime = DateTime.ParseExact("10/05/2024 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Configurar o comportamento do serviço de marcação para lançar uma exceção
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("Só pode haver uma entrada por dia"));

            // Act e Assert
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoTerceiraEntradaQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 1,
                DateTime = DateTime.ParseExact("13/05/2024 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            // Configurar o comportamento do serviço de marcação para lançar uma exceção
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("A quantidade máxima de batidas é duas por dia"));

            // Act e Assert
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }

        [Fact]
        public async Task BatidaPontoFuncionarioNaoExisteQuandoCadastraBatidaPontoEntaoDeveSerGeradoExceptionServiceError()
        {
            SetupEmployeeMarkingRepository();
            //SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            var employeeCreate = new EmployeMarkingDTO
            {
                MarkingId = 1,
                DateTime = DateTime.ParseExact("13/05/2024 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                EmployeeId = 1,
            };

            employeServiceMock
                .Setup(service => service.GetAsync(It.IsAny<int>()))
                .ThrowsAsync(new ErrorServiceException("Não foi Possivel encontrar o funcionario do ID informado"));

            // Configurar o comportamento do serviço de marcação para lançar uma exceção


            // Act e Assert
            
            await AssertThrowsErroServiceExceptionAsync(employeeCreate);
        }


        private void SetupEmployeeMarkingRepository()
        {
            employeeMarkingRepositoryMock
                .Setup(repo => repo.GetEmployeeMarkingAsync(It.IsAny<Expression<Func<EmployeeMarking, bool>>>()))
                .ReturnsAsync((Expression<Func<EmployeeMarking, bool>> predicate) =>
                {
                    var employeeMarkings = new List<EmployeeMarking>
                    {
                    new EmployeeMarking { MarkingId = 1, EmployeeId = 1, DateTime = DateTime.ParseExact("10/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 1 },
                    new EmployeeMarking { MarkingId = 1, EmployeeId = 1, DateTime = DateTime.ParseExact("13/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 2 },
                    new EmployeeMarking { MarkingId = 2, EmployeeId = 1, DateTime = DateTime.ParseExact("13/05/2024 18:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 3 },
                    new EmployeeMarking { MarkingId = 1, EmployeeId = 1, DateTime = DateTime.ParseExact("14/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 4 },
                    new EmployeeMarking { MarkingId = 2, EmployeeId = 1, DateTime = DateTime.ParseExact("14/05/2024 18:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 5 },
                    new EmployeeMarking { MarkingId = 1, EmployeeId = 1, DateTime = DateTime.ParseExact("15/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 6 },
                    new EmployeeMarking { MarkingId = 2, EmployeeId = 1, DateTime = DateTime.ParseExact("15/05/2024 18:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 7 },
                    new EmployeeMarking { MarkingId = 1, EmployeeId = 1, DateTime = DateTime.ParseExact("16/05/2024 08:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 8 },
                    new EmployeeMarking { MarkingId = 2, EmployeeId = 1, DateTime = DateTime.ParseExact("16/05/2024 18:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Id = 8 },
                    };

                    return employeeMarkings.Where(predicate.Compile());
                });

            employeeMarkingRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<EmployeeMarking>()))
                .Returns((EmployeeMarking marking) =>
                {
                    return marking;
                });
        }

        private void SetupEmployeeService()
        {
            employeServiceMock
                .Setup(service => service.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(FactoryEmploye("Teste 01"));
        }

        private void SetupMarkingService()
        {
            markingServiceMock
                .Setup(service => service.GetMarkingAsync(It.IsAny<int>()))
                .ReturnsAsync(new Marking { Id = 1, Name = "Entrada" });
        }

        private void SetupMapper()
        {
            mapperMock
                .Setup(map => map.Map<EmployeeMarking>(It.IsAny<EmployeMarkingDTO>()))
                .Returns((EmployeMarkingDTO employMarkingDTO) =>
                {
                    return new EmployeeMarking
                    {
                        MarkingId = employMarkingDTO.MarkingId,
                        EmployeeId = employMarkingDTO.EmployeeId,
                        DateTime = employMarkingDTO.DateTime,
                    };
                });
        }

        private void AssertRecordWasCreated(EmployeeMarking result, EmployeMarkingDTO employeeCreate)
        {
            Assert.Equal(employeeCreate.MarkingId, result.MarkingId);
            Assert.Equal(employeeCreate.DateTime, result.DateTime);
            Assert.Equal(employeeCreate.EmployeeId, result.EmployeeId);

            employeeMarkingRepositoryMock.Verify(er => er.Create(It.IsAny<EmployeeMarking>()), Times.Once);
        }

        private async Task AssertThrowsErroServiceExceptionAsync(EmployeMarkingDTO employeeCreate)
        {
            // Arrange
            SetupEmployeeMarkingRepository();
            SetupEmployeeService();
            SetupMarkingService();
            SetupMapper();

            // Act e Assert
            await Assert.ThrowsAsync<ErrorServiceException>(async () => await employeeMarkingService.Add(employeeCreate));
        }

        private Employee FactoryEmploye(string name)
        {
            TimeSpan start;
            TimeSpan end;
            TimeSpan.TryParse("08:00:00", out start);
            TimeSpan.TryParse("18:00:00", out end);

            return
                new Employee
                {
                    Name = name,
                    Schedule =
                        new Schedule
                        {
                            Name = "ADM",
                            Start = start,
                            End = end,
                            ScheduleDays =
                                    new ScheduleDay[]
                                    {
                                    new ScheduleDay { DayId = 2, ScheduleId = 1 },
                                    new ScheduleDay { DayId = 3, ScheduleId = 1 },
                                    new ScheduleDay { DayId = 4, ScheduleId = 1 },
                                    new ScheduleDay { DayId = 5, ScheduleId = 1 },
                                    new ScheduleDay { DayId = 6, ScheduleId = 1 }
                                    }
                        }
                };
        }
    }

}
