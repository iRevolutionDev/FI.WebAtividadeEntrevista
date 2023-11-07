using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            var bo = new BoCliente();
            var boBeneficiario = new BoBeneficiario();

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                    from error in item.Errors
                    select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistencia(model.CPF) || boBeneficiario.VerificarExistencia(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("CPF já cadastrado");
            }

            model.Id = bo.Incluir(new Cliente
            {
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                CPF = CpfConverter.ToLong(model.CPF),
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
                Beneficiarios = model.Beneficiarios?.Select(beneficiario => new Beneficiario
                {
                    IdCliente = model.Id,
                    Nome = beneficiario.Nome,
                    CPF = CpfConverter.ToLong(beneficiario.CPF)
                }).ToList()
            });

            try
            {
                model.Beneficiarios?.ForEach(beneficiario =>
                {
                    if (boBeneficiario.VerificarExistencia(beneficiario.CPF) ||
                        bo.VerificarExistencia(beneficiario.CPF))
                    {
                        Response.StatusCode = 400;
                        throw new Exception("CPF já cadastrado");
                    }

                    boBeneficiario.Incluir(new Beneficiario
                    {
                        IdCliente = model.Id,
                        Nome = beneficiario.Nome,
                        CPF = CpfConverter.ToLong(beneficiario.CPF)
                    });
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            var bo = new BoCliente();
            var boBeneficiario = new BoBeneficiario();

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                    from error in item.Errors
                    select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistencia(model.CPF) || boBeneficiario.VerificarExistencia(model.CPF))
            {
                var cliente = bo.Consultar(model.Id);

                if (cliente.CPF != CpfConverter.ToLong(model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json("CPF já cadastrado");
                }
            }

            try
            {
                var beneficiarios = boBeneficiario.Listar(model.Id) ?? new List<Beneficiario>();

                beneficiarios.ForEach(beneficiario =>
                {
                    if (model.Beneficiarios == null)
                        boBeneficiario.Excluir(beneficiario.Id);
                    else if (model.Beneficiarios.All(b => b.Id != beneficiario.Id))
                        boBeneficiario.Excluir(beneficiario.Id);
                });

                model.Beneficiarios?.ForEach(beneficiario =>
                {
                    if (boBeneficiario.VerificarExistencia(beneficiario.CPF) ||
                        bo.VerificarExistencia(beneficiario.CPF))
                    {
                        var beneficiarioExistente = boBeneficiario.Consultar(beneficiario.Id);
                        
                        if (beneficiarioExistente.CPF != CpfConverter.ToLong(beneficiario.CPF))
                        {
                            Response.StatusCode = 400;
                            throw new Exception("CPF já cadastrado");
                        }
                    }

                    if (beneficiario.Id == 0)
                        boBeneficiario.Incluir(new Beneficiario
                        {
                            IdCliente = model.Id,
                            Nome = beneficiario.Nome,
                            CPF = CpfConverter.ToLong(beneficiario.CPF)
                        });
                    else
                        boBeneficiario.Alterar(new Beneficiario
                        {
                            Id = beneficiario.Id,
                            IdCliente = model.Id,
                            Nome = beneficiario.Nome,
                            CPF = CpfConverter.ToLong(beneficiario.CPF)
                        });
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            bo.Alterar(new Cliente
            {
                Id = model.Id,
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                CPF = CpfConverter.ToLong(model.CPF),
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone
            });

            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            var bo = new BoCliente();
            var boBeneficiario = new BoBeneficiario();
            var cliente = bo.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
                model = new ClienteModel
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    CPF = CpfConverter.ToFriendlyString(cliente.CPF),
                    Telefone = cliente.Telefone,
                    Beneficiarios = boBeneficiario.Listar(id).Select(beneficiario => new BeneficiarioModel
                    {
                        Id = beneficiario.Id,
                        IdCliente = beneficiario.IdCliente,
                        Nome = beneficiario.Nome,
                        CPF = CpfConverter.ToFriendlyString(beneficiario.CPF)
                    }).ToList()
                };

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var qtd = 0;
                var campo = string.Empty;
                var crescente = string.Empty;
                var array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                var clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo,
                    crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
    }
}