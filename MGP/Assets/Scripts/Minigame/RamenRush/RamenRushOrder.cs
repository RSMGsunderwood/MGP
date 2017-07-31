using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RamenRushOrder : MonoBehaviour {

	public TextMeshProUGUI soupText;
	public List<Image> soupChecks;
	public TextMeshProUGUI meatText;
	public List<Image> meatChecks;
	public TextMeshProUGUI toppingText;
	public List<Image> toppingChecks;
	public List<Image> noodleChecks;

	public void AssignOrder(string soup, string meat, string topping){
		soupText.text = soup;
		meatText.text = meat;
		toppingText.text = topping;
	}
}
