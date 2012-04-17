

//sort type: 0 = row, 1 = column, 2 = column (for one row only)
//this number can be changed at any time (even after the page has loaded), except while actually
//dragging something
var tableSortType = 0;

//column sorting is based on TD elements only, not TH (if the table contains any TH elements, the script
//will not be able to sort all rows of the table)

//this does not work in IE 5 Mac because it incorrectly calculates positions of table cells.
//unfortunately the workaround would make this script more difficult;
//all calls to MWJ_getPosition must instead get the position of an element that is inside the TD and
//completely filling the TD - a DIV is useful here. it will still crash if you try to move elements to
//the end of their row or table.
//thankfully this browser has been almost completely replaced by Safari, so I do not recommend trying

var theTable = document.getElementById('canBeSorted');
if(theTable!=null){
    if( document.getElementById && document.createElement && document.childNodes ) {

	    for( var i = 0, x = theTable.getElementsByTagName('tr'); x[i]; i++ ) {
		    for( var j = 0, y = x[i].getElementsByTagName('td'); y[j]; j++ ) {
			    //all table cells must watch for the mouse to click on them
			    //every time you add a new cell, you must also run these two functions on them
			    MWJ_monitorButton( y[j], 'mousedown', startDrag );
			    y[j].onselectstart = function () { return false; };
		    }
	    }
    }
}

function startDrag(e,oCode,oButton,oElement) {
	if( oButton != 'left' ) { return; }
	if( !e ) { e = window.event; }
	if( e.preventDefault ) { e.preventDefault(); }
	//they have clicked down. must store position and watch to see if they drag it
	window.storeMousePos = MWJ_getMouseCoords(e);
	window.mouseIsDown = tableSortType ? oElement : oElement.parentNode;
	MWJ_monitorMouse(mouseismove);
	MWJ_monitorButton( document, 'mouseup', stopDrag );
}
function mouseismove() {
	if( !window.mouseIsDown ) { return; }
	//they have started dragging
	//moving table cells causes many problems so create a false element for them to move
	if( !window.falseElement ) {
		window.falseElement = document.createElement('div');
		falseElement.style.position = 'absolute';
		falseElement.style.width = mouseIsDown.offsetWidth + 'px';
		falseElement.style.height = mouseIsDown.offsetHeight + 'px';
		falseElement.style.border = '1px solid #00f';
		document.body.appendChild(falseElement);
	}
	//move the false element to the calculated position
	var cellXY = MWJ_getPosition( mouseIsDown );
	falseElement.style.left = tableSortType ? ( ((MWJ_getMouse[0]-storeMousePos[0])+cellXY[0])+'px' ) : ( cellXY[0]+'px' );
	falseElement.style.top = tableSortType ? ( cellXY[1]+'px' ) : ( ((MWJ_getMouse[1]-storeMousePos[1])+cellXY[1])+'px' );
}
function stopDrag() {
	if( window.falseElement ) {
		//stop monitoring the mouse
		document.onmousemove = null;
		document.body.removeChild(falseElement);
		window.falseElement = false;
		//work out what orders they should be in, based on the position of the left edge of the cell, or top position of the row
		var currentPosition = tableSortType ? ( (MWJ_getMouse[0]-storeMousePos[0])+MWJ_getPosition( mouseIsDown )[0] ) : ( (MWJ_getMouse[1]-storeMousePos[1])+MWJ_getPosition( mouseIsDown )[1] );
		//find out the index of the cell they dragged
		for( var dragCellPos = 0, x = mouseIsDown.parentNode.getElementsByTagName(tableSortType?'td':'tr'); x[dragCellPos] != mouseIsDown; dragCellPos++ ) { }
		//find out the index of the cell it must be put before (I could just swap directly, but this way, I can swap them for all rows)
		for( var i = 0, x = mouseIsDown.parentNode.getElementsByTagName(tableSortType?'td':'tr'), beforeThis = x.length; x[i]; i++ ) {
			var cellPosition = MWJ_getPosition( x[i] )[tableSortType?0:1];
			if( x[i] != mouseIsDown && currentPosition < cellPosition && !( beforeThis - x.length ) ) {
				//we are not dealing with the actual cell that is being dragged and the dragged cell must be inserted before this one
				beforeThis = i;
			}
		}
		//we now have the new ordering that is required - stored as the index of the cell it must be inserted before

		
		//prevent errors from occurring when dragged past end of table
		if (beforeThis>maxRows) beforeThis=maxRows;
		//prevent dragged row from gettgin moved above header row
		if (beforeThis==0) beforeThis=1;
		

		if( beforeThis != dragCellPos + 1 ) {
			//a move is required
			//now decide if we should re-arrange the whole table or just that row - this is up to the person who uses this script

			switch( tableSortType ) {

				case 0:
					//re-arrange rows in the body
					var trs = mouseIsDown.parentNode.getElementsByTagName('tr');
					mouseIsDown.parentNode.insertBefore(mouseIsDown,trs[beforeThis]);
					getRowsList();
					break;

				case 1:
					//re-arrange cells in all rows
					for( var i = 0, y = mouseIsDown.parentNode.parentNode.getElementsByTagName('tr'), oE; oE = y[i]; i++ ) {
						//for each row
						var tds = oE.getElementsByTagName('td');
						oE.insertBefore(tds[dragCellPos],tds[beforeThis]);
					}
					break;

				default:
					//re-arrange cells in just this row
					var tds = mouseIsDown.parentNode.getElementsByTagName('td');
					mouseIsDown.parentNode.insertBefore(mouseIsDown,tds[beforeThis]);

			}

		}

	}
	window.mouseIsDown = false;
}



function getRowsList(){
	//get the sortable table
	var element = document.getElementById('canBeSorted');
	//get the sorting values
	var formFieldList = getElementsByTagNames('input',element);
	//generate sort list
	showList(formFieldList);
	//start ajax update
	//initiatesortOrderUpdate(event); 
	//alert('5');
}
function showList(array) {
	var formvalue='';
	for (var i=0;i<array.length;i++) {
		if (i+1==array.length){
			formvalue+=array[i].value
		} else {
			formvalue+=array[i].value + ","
		}
	}
	//set the sortorder string
	
}

function getElementsByTagNames(list,obj) {
	if (!obj) var obj = document;
	var tagNames = list.split(',');
	var resultArray = new Array();
	for (var i=0;i<tagNames.length;i++) {
		var tags = obj.getElementsByTagName(tagNames[i]);
		for (var j=0;j<tags.length;j++) {
			resultArray.push(tags[j]);
		}
	}
	var testNode = resultArray[0];
	if (!testNode) return [];
	if (testNode.sourceIndex) {
		resultArray.sort(function (a,b) {
				return a.sourceIndex - b.sourceIndex;
		});
	}
	else if (testNode.compareDocumentPosition) {
		resultArray.sort(function (a,b) {
				return 3 - (a.compareDocumentPosition(b) & 6);
		});
	}
	sortorder();
	return resultArray;
}

