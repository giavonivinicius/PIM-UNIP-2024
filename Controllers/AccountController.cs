using Microsoft.AspNetCore.Mvc;
using PimUrbanGreen.Models;
using PimUrbanGreen.Repositories;

namespace PimUrbanGreen.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly PedidoRepository _pedidoRepository;

        public AccountController(
            UserRepository userRepository, 
            ProdutoRepository produtoRepository, 
            PedidoRepository pedidoRepository)
        {
            _userRepository = userRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
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
                var user = _userRepository.GetUserByCredentials(model.Usuario, model.Senha);
                if (user != null)
                {
                    // Login bem-sucedido
                    TempData["User"] = user.Usuario;
                    return RedirectToAction("Wellcome");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Wellcome()
        {
            var produtos = _produtoRepository.GetAllProdutos();
            ViewBag.Produtos = produtos;
            ViewBag.User = TempData["User"]?.ToString();
            return View(new ItemPedidoModel());
        }

        [HttpPost]
        public IActionResult PlacePedido(ItemPedidoModel model)
        {
            if (ModelState.IsValid)
            {
                _pedidoRepository.AddItemPedido(model);
                ViewBag.Message = "Pedido realizado com sucesso!";
            }
            else
            {
                ViewBag.Message = "Erro ao realizar o pedido.";
            }

            var produtos = _produtoRepository.GetAllProdutos();
            ViewBag.Produtos = produtos;
            return View("Wellcome", model);
        }
    }
}
    