using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Cell : Cell
{
    public bool claimable;
    
    void Awake() {
        claimable = false;
    }

	protected override void ClaimCell(Cannonball cannonball) {
        if (this.claimable) {
            base.ClaimCell(cannonball);
            this.claimable = false;
            // Set cell colour to player's colour
            GetComponentInChildren<MeshRenderer>().material = cannonball.player.playerMaterial;
            // Set cell above this one, if there is one, to be claimable
            Cell cellAbove = board.GetRelativeCell(this, Direction.UP);
            if (cellAbove) {
                ((C4Cell)cellAbove).claimable = true;
            }
        }
	}
}
