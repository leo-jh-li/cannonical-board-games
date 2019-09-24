using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Cell : Cell
{
    public bool claimable;
    
    void Awake() {
        claimable = false;
    }

    public void SetCellMaterial(Material material) {
        GetComponentInChildren<MeshRenderer>().material = material;
    }

	protected override void ClaimCell(Cannonball cannonball) {
            Debug.Log(this.coord);

        if (this.claimable) {
            base.ClaimCell(cannonball);
            this.claimable = false;
            // Set cell colour to player's colour
            SetCellMaterial(cannonball.player.playerMaterial);
            // Set cell above this one, if there is one, to be claimable
            Cell cellAbove = board.GetRelativeCell(this, Direction.UP);
            if (cellAbove) {
                ((C4Cell)cellAbove).claimable = true;
            }
        }
	}
}
