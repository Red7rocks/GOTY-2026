using Godot;
using System;
using System.Collections.Generic;

public partial class Party : Node
{
	List<CharacterBody2D> members = new List<CharacterBody2D>();	//Vector containing current party members, that can be added to or removed from as needed
	List<Vector2> positions = new List<Vector2>();				//Vector Containing last (numPlayers) x (followSpacing) Positions
	Vector2 lastRecordedPosition;								//Keep track of our previous positions for trailing followers

	int followSpacing = 15;		//How far apart players should follow. can be adjusted
	int playerSpeed = 7;		//How fast players can move. can be adjusted

	public void CreateCharacter(PackedScene character){
		CharacterBody2D newCharacter = character.Instantiate<CharacterBody2D>();	//Create new CharacterBody from PackedScene that was passed
		members.Add(newCharacter);												//Add created character to the partyd
	}
	public void MoveLeader(){
		if (members.Count == 0)
			return;
		Vector2 direction = Input.GetVector("a", "d", "w", "s");	//Grab input from WASD keys 
		members[0].Velocity = direction * playerSpeed;				//Set leader movement to whatever input was read
		members[0].MoveAndCollide(members[0].Velocity);					//Actually move the player
	}
	public void recordPosition(){
		if (positions.Count == 0 || members[0].GlobalPosition.DistanceTo(lastRecordedPosition) > 0){	//Record current position if no positions have been recorded yet,
			positions.Insert(0, members[0].GlobalPosition);											//or if distance from last recorded position is non-zero
			lastRecordedPosition = members[0].GlobalPosition;
		}
		int maxPositions = followSpacing * members.Count;		//We give ourselves as many position points in our vector as there are spaces between players
		if (positions.Count > maxPositions){				//Memory management so vector doesn't grow infinitely
			positions.RemoveAt(positions.Count - 1);
		}
	}
	public void MoveFollowers(){
		for (int i = 1; i < members.Count; i++){
			int index = i * followSpacing;				//Setting index of current player to where leading player was 'followingSpacing' positions ago
			if (index < positions.Count){							//Make sure position exists. > Count would be outside of the array
				members[i].GlobalPosition = positions[index]; 		//Move character[i] to next position in the index
			}
		}
	}
	public int getPartyCount(){
		return members.Count;		//Get number of players in the party
	}
	public CharacterBody2D getCharacter(int member){
		return members[member];		//Get specific character in the party
	}
	public void AddPlayersToScene(Node parentNode){
		foreach (var character in members){
			if (!GodotObject.IsInstanceValid(character)){	//Check that object we are attempting to add is a valid object
				continue;
			}
			if (character.GetParent() == null){
				parentNode.AddChild(character);				//reparent will fail if character does not already have a parent. use Add child in this case
			} else {
				character.Reparent(parentNode, true);		//Reassign parent of charactes to scene/node specified
			}	
		}
	}
	public void setPartyPosition(Vector2 position){
		if (members.Count == 0)
			return;
		for (int i = 0; i < members.Count; i++){
			members[i].GlobalPosition = position;		//Set all players to specified position
		}
		positions.Clear();					//Clear current list of positions
		lastRecordedPosition = position;
		for (int i = 0; i < followSpacing * members.Count; i++){
			positions.Add(position);		// Set all current/past positions to specified value so all players are in sync
		}
	}
}
