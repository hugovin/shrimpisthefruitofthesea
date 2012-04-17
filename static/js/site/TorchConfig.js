var torchPrice = 0;
var torchSku = 0;
var lowPrice = 0;
var PricesAndSku = new Array();
var configFirst = 'Setup Your Torch <img src="images/configButton.jpg" onclick="javascript:gotomarc(1);" class="confButton" />';

function SelectSubject() {
	$('ELL_EDITION').checked = false;
	var subjetvalue = $$('input[name=SUBJECT]:checked')[0].get('value');
	var gradevalue = $('GRADE').get('value');
	setPriceAndSku(gradevalue, subjetvalue);
}

function SelectGrade() {
	$('conf_price').set('html', configFirst);
	var gradevalue = $('GRADE').get('value');
	    if (gradevalue == '2') {
	    	$('SUBJECT_ALL').set('disabled', false).set('checked', false);
	    	$('SUBJECT_MTH').set('disabled', false).set('checked', false);
	    	$('SUBJECT_SCI').set('disabled', false).set('checked', false);
	    	$('SUBJECT_SOC').set('disabled', false).set('checked', false);
	    	$('ELL_EDITION').checked = false;
	    	lowPrice = 0;
	    	torchPrice = 0;
	    }

	    if (gradevalue == '1')
	     {
	     	$('SUBJECT_ALL').set('disabled', true).set('checked', false);
	     	$('SUBJECT_MTH').set('disabled', false).set('checked', false);
	     	$('SUBJECT_SCI').set('disabled', false).set('checked', false);
	     	$('SUBJECT_SOC').set('disabled', false).set('checked', false);
	     	$('ELL_EDITION').checked = false;
	     	lowPrice = 0;
	     	torchPrice = 0;
	    }
}

function ELLSelected() {
	var price = $('conf_price').get('html');
	if (price.indexOf("Setup") == -1) {
		if (lowPrice == 0) {
			var subjetvalue = $$('input[name=SUBJECT]:checked')[0].get('value');
			var gradevalue = $('GRADE').get('value');
			var newSubject = (subjetvalue == 1) ? 3 : 4;
			setPriceAndSku(gradevalue, newSubject);
			lowPrice = 1;
		} else {
			var subjetvalue = $$('input[name=SUBJECT]:checked')[0].get('value');
			var gradevalue = $('GRADE').get('value');
			setPriceAndSku(gradevalue, subjetvalue);
			lowPrice = 0;
		}
	} else {
	    $('ELL_EDITION').checked = false;
	}
}

function setPriceAndSku(grade, subject) {
	for (var i = 0; i < SkuContainers.length; i++) {
		if (SkuContainers[i].subjectid == subject && SkuContainers[i].gradeid == grade) {
			$('conf_price').set('html', '$' + SkuContainers[i].price);
			$('newSku').setProperty('value', SkuContainers[i].sku);
		}
	    
	}
}
function displayTooltip() {
	$('tooltip').set('class', 'tooltip');
}

function hideTooltip() {
	$('tooltip').set('class', 'tooltiphide');
}

function whiteBoardYes() {
	$('whiteboard-brand').set('class', 'sub-option1Display');
}


function whiteBoardNo() {
	$('whiteboard-brand').set('class', 'sub-option1');
}

function WhiteBrand(ele) {
	if (ele.value == "OTHR") {
		$('whiteboardbrandother').set('class', 'sub-option2Display');
	} else {
	    $('whiteboardbrandother').set('class', 'sub-option1');
	}
}

function ResponseYes() {
	$('lrs-brand').set('class', 'sub-option1Display');
}


function ResponseNo() {
	$('lrs-brand').set('class', 'sub-option1');
}


function ResponseSysChoose(ele) {
	if (ele.value == "OTHR") {
		$('lrsbrandother').set('class', 'sub-option2Display');
	} else {
	$('lrsbrandother').set('class', 'sub-option1');
	}
}