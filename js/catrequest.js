function ltrim(s) {
         return s.replace(/^\s+/, "");
     }
     function rtrim(s) {
         return s.replace(/\s+$/, "");
     }
     function trim(s) {
         return rtrim(ltrim(s));
     }
     function strim(s){
         return s.replace(/ /g, '');
     }
        
        
    function hideAllAlerts(){
        for(i = 1; i<9;i++){
            document.getElementById('alert'+i).style.display="none";
            document.getElementById('errorGlobal').style.display="none";
        }
        for(i = 1; i<9;i++){
            cambia_clase('fieldName'+i,'formLabel');
        }
    }
    function showAllAlerts(){
        for(i = 1; i<9;i++){
            document.getElementById('alert'+i).style.display="block";
            document.getElementById('errorGlobal').style.display="block";
        }
        for(i = 1; i<9;i++){
            cambia_clase('fieldName'+i,'formLabel alert');
        }
    }
    
    function cambia_clase(id_del_objeto,nueva_clase){
          var objeto = document.getElementById(id_del_objeto);
          objeto.className = nueva_clase;
    }
    
    function alertField(numberField){
            document.getElementById('alert'+numberField).style.display="block";
            cambia_clase('fieldName'+numberField,'formLabel alert');
            document.getElementById('errorGlobal').style.display="block";
    }
    function validForm(){
        hideAllAlerts();
        if(!validFullName()){
            return false;
        }
        if(!validJob()){
            return false;
        }
        if(!validSchool()){
            return false;
        }
        if(!validPurchase()){
            return false;
        }
        if(!validStreet()){
            return false;
        }
        if(!validCity()){
            return false;
        }
        if(!validState()){
            return false;
        }
        if(!validZip()){
            return false;
        }
        return false;
    }
    function validFullName(){
        var name = trim(document.getElementById('_ctl0__ctl1__ctl0__FirstName').value);
        //alert(name);
        if(name!= ""){
            return true;
        }else{
            alertField(1);
            return false;
        }
    }    
    function validJob(){
        var job = trim(document.getElementById('_ctl0__ctl1__ctl0_ddlTitle').value);
         if(job!= ""){
            return true;
        }else{
            alertField(2);
            return false;
        }
    }
    function validSchool(){
        var sc = trim(document.getElementById('_ctl0__ctl1__ctl0__CompanyName').value);
         if(sc!= ""){
            return true;
        }else{
            alertField(3);
            return false;
        }
    }
    function validPurchase(){
        var ps = trim(document.getElementById('_ctl0__ctl1__ctl0_ddlPurchaseFor').value);
         if(ps!= ""){
            return true;
        }else{
            alertField(4);
            return false;
        }
    }
    function validStreet(){
        var st = trim(document.getElementById('_ctl0__ctl1__ctl0__Address1').value);
         if(st!= ""){
            return true;
        }else{
            alertField(5);
            return false;
        }
    }
    function validCity(){
        var ct = trim(document.getElementById('_ctl0__ctl1__ctl0__City').value);
         if(ct!= ""){
            return true;
        }else{
            alertField(6);
            return false;
        }
    }
    function validState(){
        var ct = trim(document.getElementById('_ctl0__ctl1__ctl0__State').value);
         if(ct!= ""){
            return true;
        }else{
            alertField(7);
            return false;
        }
    }
    function validZip(){
        var zp = trim(document.getElementById('_ctl0__ctl1__ctl0__Zip').value);
         if(zp!= ""){
            if(zip.length>4){
                    if(!isNaN(zp)){
                        return true;
                    }else{
                            alertField(8);
                            return false;
                    }
            }else{
                alertField(8);
                return false;
            }
        }else{
            alertField(8);
            return false;
        }
    }