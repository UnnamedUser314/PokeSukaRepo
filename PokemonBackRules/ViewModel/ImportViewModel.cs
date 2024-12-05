using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PokemonBackRules.Interfaces;
using PokemonBackRules.Model;
using PokemonBackRules.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PokemonBackRules.ViewModel
{
    public partial class ImportViewModel : ViewModelBase
    {
        private readonly IFileService<PokemonDataModel> _fileService;
        public ImportViewModel(IFileService<PokemonDataModel> fileService)
        {
            _fileService = fileService;
           

        }

        public override async Task LoadAsync()
        {
            var openFileDialog = new OpenFileDialog();


        }

        [RelayCommand]
        public void LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var loadedContacts = _fileService.Load(openFileDialog.FileName);
                HttpJsonClient<PokemonApiModel>.DeleteAll(Constantes.POKE_TEAM_URL);
                foreach (var contact in loadedContacts)
                {
                    HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, contact);
                    
                }
            }
        }
    }
}
