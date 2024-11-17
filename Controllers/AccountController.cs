using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Adicionado para suportar ISession
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
                var user = _userRepository.GetUserByCredentials(model.Usuario, model.Senha);
                if (user != null)
                {
                    // Login bem-sucedido: Armazena o usuário na sessão
                    HttpContext.Session.SetString("User", user.Usuario);
                    _logger.LogInformation($"Usuário logado com sucesso: {user.Usuario}");
                    return RedirectToAction("Wellcome");
                }
                else
                {
                    _logger.LogWarning("Tentativa de login falhou: Usuário ou senha inválidos.");
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

            // Recupera o usuário logado da sessão
            var loggedUser = HttpContext.Session.GetString("User");
            ViewBag.User = loggedUser;

            _logger.LogInformation($"Bem-vindo exibido para o usuário: {loggedUser}");

            return View(new ItemPedidoModel());
        }
        
        [HttpPost]
        public IActionResult PlacePedido(ItemPedidoModel model)
        {
            // Recupera o usuário logado da sessão
            var loggedUser = HttpContext.Session.GetString("User");
            _logger.LogInformation($"Tentativa de inserir pedido pelo usuário: {loggedUser}");

            if (!string.IsNullOrEmpty(loggedUser))
            {
                model.Usuario = loggedUser;
                _logger.LogInformation($"Campo Usuario do modelo preenchido com: {loggedUser}");
            }
            else
            {
                _logger.LogWarning("Nenhum usuário logado identificado na sessão.");
            }

            if (ModelState.IsValid)
            {
                _pedidoRepository.AddItemPedido(model);
                ViewBag.Message = "Pedido realizado com sucesso!";
                _logger.LogInformation($"Pedido realizado com sucesso para o usuário: {loggedUser}");
            }
            else
            {
                ViewBag.Message = "Erro ao realizar o pedido.";
                _logger.LogWarning("Erro ao realizar o pedido: ModelState inválido.");
            }

            var produtos = _produtoRepository.GetAllProdutos();
            ViewBag.Produtos = produtos;

            return View("Wellcome", model);
        }
    }
}
