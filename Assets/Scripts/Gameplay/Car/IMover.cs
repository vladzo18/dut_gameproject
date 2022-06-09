namespace Gameplay.Car {
    
    public interface IMover {
        
        public bool IsMoving { get; }
        public float CurrentEngineSpeed { get; }
        public float MaxSpeed { get;  }
        public void MoveRight();
        public void MoveLeft();
        public void StopMoving();
        public void SetMovementAbility(bool isAble);
        public void SetEngineForce(float value);

    }
    
}