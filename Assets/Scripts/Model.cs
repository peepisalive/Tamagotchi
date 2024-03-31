using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Core.Job;
using Utils;
using Core;

namespace Tamagotchi
{
    public sealed class Model : IEcsSystem
    {
        private EcsWorld _world;

        private EcsFilter<BlockComponent, Active> _activeBlockFilter;
        private EcsFilter<BlockComponent> _blockFilter;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private EcsFilter<JobComponent> _jobFilter;
        private EcsFilter<PetComponent> _petFilter;

        #region Navigation
        public IEnumerable<NavigationPoint> GetChildPointsOfType(NavigationBlockType blockType, NavigationElementType type)
        {
            return _blockFilter.GetChildPointsOfType(blockType, type);
        }

        public NavigationBlockType? GetCurrentBlockType()
        {
            return _activeBlockFilter.GetCurrentBlockType();
        }

        public NavigationBlock GetCurrentNavigationBlock()
        {
            return _activeBlockFilter.GetCurrentNavigationBlock();
        }

        public NavigationPoint GetCurrentNavigationPoint()
        {
            return _activeBlockFilter.GetCurrentNavigationPoint();
        }
        #endregion

        public Pet GetCurrentPet()
        {
            foreach (var i in _petFilter)
            {
                return _petFilter.Get1(i).Pet;
            }

            return null;
        }

        public HashSet<Job> GetAvailableJob()
        {
            foreach (var i in _jobFilter)
            {
                return _jobFilter.Get1(i).AvailableJob;
            }

            return null;
        }

        public BankAccount GetBankAccount()
        {
            foreach (var i in _bankAccountFilter)
            {
                return _bankAccountFilter.Get1(i).BankAccount;
            }

            return null;
        }

        public void Send<T>(T component) where T : struct
        {
            _world.NewEntity().Replace(component);
        }
    }
}