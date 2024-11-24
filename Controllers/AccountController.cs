using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PimUrbanGreen.Models;
using PimUrbanGreen.Repositories;

namespace PimUrbanGreen.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly PedidoRepository _pedidoRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserRepository userRepository, 
            ProdutoRepository produtoRepository, 
            PedidoRepository pedidoRepository, 
            ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByCredentials(model.NomeUsuario, model.Senha);
                if (user != null)
                {
                    HttpContext.Session.SetString("User", user.NomeUsuario);
                    return RedirectToAction("Wellcome");
                }
                ModelState.AddModelError("", "Usuário ou senha inválidos.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Wellcome()
        {
            ViewBag.Produtos = _produtoRepository.GetAllProdutos();
            ViewBag.User = HttpContext.Session.GetString("User");
            return View(new PedidoWebModel());
        }

        [HttpPost]
        public IActionResult PlacePedido(PedidoWebModel model)
        {
            var loggedUser = HttpContext.Session.GetString("User");
            if (loggedUser != null)
            {
                model.UsuarioPedido = loggedUser;
                _pedidoRepository.AddPedidoWeb(model);
                ViewBag.Message = "Pedido realizado com sucesso!";
            }
            else
            {
                ViewBag.Message = "Erro ao identificar o usuário.";
            }

            ViewBag.Produtos = _produtoRepository.GetAllProdutos();
            return View("Wellcome", model);
        }
    }
}
