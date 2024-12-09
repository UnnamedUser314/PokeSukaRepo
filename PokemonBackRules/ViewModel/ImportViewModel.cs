using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IHttpJsonClientProvider<PokemonApiModel> _httpJsonClientProvider;

        [ObservableProperty]
        private string _imagePath;

        public ImportViewModel(IFileService<PokemonDataModel> fileService, IHttpJsonClientProvider<PokemonApiModel> httpJsonClientProvider)
        {
            _fileService = fileService;
           _httpJsonClientProvider = httpJsonClientProvider;

        }

        public override async Task LoadAsync()
        {
            var openFileDialog = new OpenFileDialog();


        }

        [RelayCommand]
        public async void LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var loadedContacts = _fileService.Load(openFileDialog.FileName);
                //if loadedContacts null flecha

                if (loadedContacts != null) {
                    ImagePath = Constantes.SUCCESS_IMAGE_PATH;
                } else
                {
                    ImagePath = Constantes.ERROR_IMAGE_PATH;
                }

                var response = HttpJsonClient<PokemonApiModel>.DeleteAll(Constantes.POKE_TEAM_URL);

                foreach (var contact in loadedContacts)
                {
                    _httpJsonClientProvider.Post(Constantes.POKE_TEAM_URL, contact);
                    
                }
                

            }
        }
    }
}
