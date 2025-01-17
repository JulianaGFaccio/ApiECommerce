﻿using ApiECommerce.Context;
using ApiECormmerce.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public PedidosController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/Pedidos/DetalhesPedido/5
    // Retorna os detalhes de um pedido específico, incluindo informações sobre
    // os produtos associados a esse pedido.
    [HttpGet("[action]/{pedidoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public async Task<IActionResult> DetalhesPedido(int pedidoId)
    {
        var pedidoDetalhes = await (from detalhePedido in dbContext.DetalhesPedido
                                    join pedido in dbContext.Pedidos on detalhePedido.PedidoId equals pedido.Id
                                    join produto in dbContext.Produtos on detalhePedido.ProdutoId equals produto.Id
                                    where detalhePedido.PedidoId == pedidoId
                                    select new
                                    {
                                        Id = detalhePedido.Id,
                                        Quantidade = detalhePedido.Quantidade,
                                        SubTotal = detalhePedido.ValorTotal,
                                        ProdutoNome = produto.Nome,
                                        ProdutoImagem = produto.UrlImagem, 
                                        ProdutoPreco = produto.Preco   
                                    }).ToListAsync();

        if (pedidoDetalhes == null || pedidoDetalhes.Count == 0)
        {
            return NotFound("Detalhes do pedido não encontrados.");
        }

        return Ok(pedidoDetalhes);
    }

    // Neste codigo a criação do pedido, a adição dos detalhes do pedido
    // e a remoção dos itens do carrinho são agrupados dentro de uma transação única.
    // Se alguma operação falhar, a transação será revertida e nenhuma alteração será
    // persistida no banco de dados. Isso garante a consistência dos dados e evita a
    // possibilidade de criar um pedido sem itens no carrinho ou deixar itens
    // no carrinho após criar o pedido.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Pedido pedido)
    {
        pedido.dataPedido = DateTime.Now;

        var itemCarrinho = await dbContext.ItensCarrinhoCompra
            .Where(carrinho => carrinho.ClienteId == pedido.UsuarioId)
            .ToListAsync();
        // Verifica se ha itens no carrinho

        if (itemCarrinho.Count == 0)
        {
            return NotFound("Não há itens no carrinho para criar o pedido");
        }

        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                dbContext.Pedidos.Add(pedido);
                await dbContext.SaveChangesAsync();

                foreach (var item in itemCarrinho)
                {
                    var detalhePedido = new DetalhePedido()
                    {
                        Preco = item.PrecoUnitario,
                        ValorTotal = item.ValorTotal,
                        Quantidade = item.Quantidade,
                        ProdutoId = item.ProdutoId,
                        PedidoId = pedido.Id
                    };
                    dbContext.DetalhesPedido.Add(detalhePedido);
                }

                await dbContext.SaveChangesAsync();
                dbContext.ItensCarrinhoCompra.RemoveRange(itemCarrinho);
                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new { OrderId = pedido.Id });

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return BadRequest("Ocorreu um erro ao processar o pedido.");
            }
        }
    }

}
