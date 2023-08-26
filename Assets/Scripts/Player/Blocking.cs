using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    // Called when collision with anything occurs
    private void OnCollisionEnter(Collision collision) {
        
        // CHECK: If counter is done correctly to the beat, continue. Else, punish player and deal the entity's damage to player immediately
        // if counter done correctly then

            // Check that the type of object colliding is enemy weapon/ranged attacks
            if (collision.gameObject.name == "MELEEATTACK") {
                // Do not damage player, if ranged atk, the atk despawns.
            } else if (collision.gameObject.name == "RANGEDATTACK") {
                // if ranged weapon then: Destroy(collision.gameObject);
                // if player is melee: auto swing one attack, dealing more damage.
            }
            // BuffPlayer(); should also have invincibility frames for 2 seconds
        // else { player.health -= collision.gameObject.DAMAGE? }
    }
}
