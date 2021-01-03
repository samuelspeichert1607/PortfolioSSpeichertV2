<?php
    $link = mysqli_connect("localhost", "root", "", "tp2_bd_portair");   
	if (mysqli_connect_errno()) {
		printf("Connexion échouée : %s\n", mysqli_connect_error());
		exit();
	}                   
    if(isset($_POST['submitBtn']))
    {                  
                  $id = uniqid('prefix');
                  $date = $_POST['dateEnvolee'];
                  $vol = $_POST['dropDownVol'];
                  $segment = $_POST['dropDownSegment'];
                  $pilote = $_POST['dropDownPilote'];
                  $avion = $_POST['dropDownAvion'];
                  
                  // Query the database for user
                  $result = mysqli_query("INSERT INTO `envolee` (`Envolee_id`, `Envolee_date`, `pilote_idPilote`, `avion_Avion_id`, `vol_has_segment_vol_Vol_id`, `vol_has_segment_segment_Segment_id`) VALUES ('5', '$date', '$pilote', '$avion', '$vol', '$segment');");                        
                  //or die("Failed to query databse".mysqli_error());    
                   echo "Success";                                                                                      
                  
                                
    }
    else
    {
    echo "fail...";
    }
    
    function DropDownMaker($tab, $dropDownName)
    {
        echo "<select name=".$dropDownName.">";
        echo "<option value=>Sélectionez...</option>";
        for($i = 0; $i < sizeof($tab); $i++)
        {        
            echo"<option value=". $tab[$i] .">".$tab[$i]."</option>";
        }
        echo "</select>";
    }     
?>
<!-- Formulaire de BD -->
<form method="POST" action="entreenvolee.php" >
	<p>
		<label>Saisir la date :</label>
		<input type ="date" name="dateEnvolee">
	</p>
	
	<p>
		<label>Saisir le numéro de vol :</label>
		<?php
			$query = "SELECT * FROM vol";
			$volArray;
			if($result = mysqli_query($link, $query)){
				while($row = mysqli_fetch_assoc($result)){
					$volArray[] = $row["Vol_id"];
				}
			}
			DropDownMaker($volArray, "dropDownVol");
		?>
	</p>
	
	<p>	
		<label>Saisir votre segement :</label>
		<?php
			$query = "SELECT * FROM vol_has_segment WHERE vol_Vol_id = 1822";
			$segmentArray;
			if($result = mysqli_query($link, $query)){
				while($row = mysqli_fetch_assoc($result)){
				$segmentArray[] = $row["segment_Segment_id"];
				}
			}
			DropDownMaker($segmentArray, "dropDownSegment");
		?>
	</p>
	
	<p>
		<label>Saisir le pilote :</label>
		<?php
			$query = "SELECT * FROM pilote";
			$piloteArray;
			if($result = mysqli_query($link, $query)){
				while($row = mysqli_fetch_assoc($result)){
					$piloteArray[] = $row["idPilote"];
				}
			}
			DropDownMaker($piloteArray, "dropDownPilote");
		?>
	</p>
	
	<p>
		<label>Saisir l'avion :</label>
		<?php
			$query = "SELECT * FROM avion";
			$avionArray;
			if($result = mysqli_query($link, $query)){
				while($row = mysqli_fetch_assoc($result))
				{
					$avionArray[] = $row["Avion_id"];
				}
			}
			DropDownMaker($avionArray, "dropDownAvion");
		?>
	</p>
	<input type="submit" name="submitBtn">
</form>
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
