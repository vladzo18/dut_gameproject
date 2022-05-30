namespace Gameplay.Car {
    
    public interface IMover {
        
        public bool IsMoveing { get; }
        public void MoveRight();
        public void MoveLeft();
        public void StopMoveing();
        public void SetMovementAbility(bool isAble);

    }
    
}