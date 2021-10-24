using MySqlConnector;
using Newtonsoft.Json;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ServidorTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.Load();

            APIServer api = new APIServer(1890);

            api.AddAction("/usuarios/me", Handlers.Usuarios.BuscarLogado.ProcessContext);
            api.AddAction("/usuarios/porid", Handlers.Usuarios.BuscarPorID.ProcessContext);
            api.AddAction("/usuarios/porcpf", Handlers.Usuarios.BuscarPorCPF.ProcessContext);
            api.AddAction("/usuarios/criar/pessoafisica", Handlers.Usuarios.CriarPessoaFisica.ProcessContext);
            api.AddAction("/usuarios/criar/pessoajuridica", Handlers.Usuarios.CriarPessoaJuridica.ProcessContext);

            api.AddAction("/categorias/criar", Handlers.Categorias.CriarCategoria.ProcessContext);
            api.AddAction("/categorias/listar", Handlers.Categorias.ListarCategorias.ProcessContext);
            api.AddAction("/categorias/atualizar/nome", Handlers.Categorias.AtualizarCategoria.ProcessContext);

            api.AddAction("/produtos/criar", Handlers.Produtos.CriarProduto.ProcessContext);
            api.AddAction("/produtos/buscar/porid", Handlers.Produtos.BuscarPorID.ProcessContext);
            api.AddAction("/produtos/buscar/imagem", Handlers.Produtos.BuscarFotoProduto.ProcessContext);
            api.AddAction("/produtos/listar/porcategoria", Handlers.Produtos.BuscarProdutosPorCategoria.ProcessContext);

            api.AddAction("/anuncios/criar", Handlers.Anuncios.CriarAnuncio.ProcessContext);
            api.AddAction("/anuncios/buscar/porid", Handlers.Anuncios.BuscarPorID.ProcessContext);
            api.AddAction("/anuncios/atualizar/titulo", Handlers.Anuncios.AtualizarTitulo.ProcessContext);
            api.AddAction("/anuncios/listar/porcategoria", Handlers.Anuncios.ListarAnuncios.ProcessContext);
            api.AddAction("/anuncios/atualizar/descricao", Handlers.Anuncios.AtualizarDescricao.ProcessContext);

            api.AddAction("/enderecos/criar", Handlers.Enderecos.CriarEndereco.ProcessContext);
            api.AddAction("/enderecos/listar", Handlers.Enderecos.BuscarPorProprietario.ProcessContext);

            api.AddAction("/estados/listar", Handlers.Enderecos.ListarEstados.ProcessContext);

            api.AddAction("/comentarios/criar", Handlers.Comentarios.CriarComentario.ProcessContext);
            api.AddAction("/comentarios/deletar", Handlers.Comentarios.DeletarComentario.ProcessContext);
            api.AddAction("/comentarios/listar/poranuncio", Handlers.Comentarios.ListarComentarios.ProcessContext);

            api.AddAction("/avaliacoes/criar", Handlers.Avaliacoes.CriarAvaliacao.ProcessContext);
            api.AddAction("/avaliacoes/listar", Handlers.Avaliacoes.ListarAvaliacoes.ProcessContext);
            api.AddAction("/avaliacoes/deletar", Handlers.Avaliacoes.DeletarAvaliacao.ProcessContext);
            api.AddAction("/avaliacoes/atualizar/pontuacao", Handlers.Avaliacoes.AtualizarPontuacao.ProcessContext);
            api.AddAction("/avaliacoes/atualizar/comentario", Handlers.Avaliacoes.AtualizarComentario.ProcessContext);

            api.AddAction("/vendas/comprar", Handlers.Vendas.EfetuarCompra.ProcessContext);
            api.AddAction("/vendas/listar/compras", Handlers.Vendas.ListarComprasPessoais.ProcessContext);
            api.AddAction("/vendas/listar/vendas", Handlers.Vendas.ListarVendasPessoais.ProcessContext);
            api.AddAction("/vendas/listar/poranuncio", Handlers.Vendas.ListarVendasPorAnuncio.ProcessContext);

            api.AddAction("/login/logar", Handlers.Login.LogarUsuario.ProcessContext);
            api.AddAction("/login/deslogar", Handlers.Login.DeslogarUsuario.ProcessContext);
            api.Listen();
        }
    }
}
