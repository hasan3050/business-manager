using System;
using System.Net;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.ViewModels
{
    public class CreateRegionViewModel:ViewModelBase
    {
        List<Region> RegionCollection = new List<Region>();//List to save the regions
        BusyWindow Busy = new BusyWindow();
        AgroDomainContext _context = new AgroDomainContext();

        public CreateRegionViewModel()
        {
            newRegion = CreateNewRegion();
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
            LoadRegions(); //This operation is done to load the regions into collection RegionCollection
           
        }

        private Region CreateNewRegion()
        {
            Region newregion = new Region();
            return newregion;
        }

        private void LoadRegions()
        {

            LoadOperation<Region> loadRegion = _context.Load(_context.GetRegionsQuery()); 

            loadRegion.Completed += (s, arg) =>
            {
                
                  RegionCollection = loadRegion.Entities.ToList();
                
            };
        
        
        }

        private void Save()
        {
         
            if (RegionCollection.Any(e => e.RegionName == newRegion.RegionName))
            {
                MessageBox.Show("This region is already created before!!");//Have to change this later
                //Show error "The Region is Already Created"
                return;
            
            }
            _context.Regions.Add(newRegion);
            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
        
        
        }


        private void Reset()
        {
            NewRegion = CreateNewRegion(); 
        
        
        }


        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            if (op.HasError)
            {
                MessageBox.Show("Failed,Please Try again!!");
                op.MarkErrorAsHandled();
                _context.RejectChanges();
            }
            else
            {
                MessageBox.Show("New Region Created");
                Reset();
                Messenger.Default.Send<string, CreateRegionChildWindow>("message");
            }
        
        
        }

        public EntitySet<Region> Regions { get { return _context.Regions; } }

        private Region newRegion;

        public Region NewRegion
        {
            get
            {
                return newRegion;
            }

            set
            {
                if (newRegion == value)
                {
                    return;
                }

                newRegion = value;
                RaisePropertyChanged("NewRegion");
            }
        }



        public ICommand SaveCommand { get; set; } //Command For Save Button 
        public ICommand ResetCommand { get; set; }//Command For Reset  Button

    }
}
