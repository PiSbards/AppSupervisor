using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppSurpervisor.View;
using AppSurpervisor.Services;
using AppSurpervisor.Models;
using System.Numerics;

namespace AppSurpervisor.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            atualizaLista();
        }
        public void atualizaLista()
        {
            string filtro = "";
            if (txtNome.Text != null) filtro = txtNome.Text;
            ServiceDbFuncionario dbFunc = new ServiceDbFuncionario(App.DbPath);
            ListaFunc.ItemsSource = dbFunc.Localizar(filtro);
        }        

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            atualizaLista();
        }

        private void ListaFunc_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Funcionario func = (Funcionario)ListaFunc.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(func));
        }

        private void pckSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string setor = pckSetor.SelectedItem.ToString();
            string turno = pckTurno.Title.ToString();
            ServiceDbFuncionario dbFunc = new ServiceDbFuncionario(App.DbPath);
            if (turno != pckTurno.Title.ToString())
            {
                turno = pckTurno.SelectedItem.ToString();
                
                ListaFunc.ItemsSource = dbFunc.Localizar(setor,turno);
            }
            else
            {
                ListaFunc.ItemsSource = dbFunc.Localizar(setor);
            }            
            
        }

        private void pckTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string Turno = pckTurno.SelectedItem.ToString();
            string setor = pckSetor.Title.ToString();
            ServiceDbFuncionario dbFunc = new ServiceDbFuncionario(App.DbPath);
            if (setor != pckSetor.Title.ToString())
            {
                setor = pckTurno.SelectedItem.ToString();

                ListaFunc.ItemsSource = dbFunc.Localizar(setor, Turno);
            }
            else
            {
                ListaFunc.ItemsSource = dbFunc.Localizar(Turno);
            }
            
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}