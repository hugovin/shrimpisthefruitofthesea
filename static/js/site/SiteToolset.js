///////////////////////////////////////////////////////////////////////// Duncan
function save_shitpping_address(FormID,RequestURL){
    //mootools test
   
   var ResponseOBJ = new Request.HTML({url:'save/',isSuccess:set_saved_saddress}).post($(FormID)); 
   alert (ResponseOBJ);
 
   
}
function set_saved_saddress(){
    var ElementToChange;
    alert ('Address Saved');
}
function display_object_contends(OBJ){

}