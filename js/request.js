
        function cambia_clase(id_del_objeto,nueva_clase){
           var objeto = document.getElementById(id_del_objeto);
           objeto.className = nueva_clase;
        }
        
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
        function eraseASpaces(fieldSpace){
            document.getElementById(fieldSpace).value = strim(document.getElementById(fieldSpace).value);
        }
        function eraseSpaces(fieldSpace){
            document.getElementById(fieldSpace).value = trim(document.getElementById(fieldSpace).value);
        }
        function validName(){
            var name = trim(document.getElementById('FullName').value);
            document.getElementById('FullName').value = name;
            if(name != ""){
                return true;    
            }else{
                //Message
                //alert('Name required.');
                document.getElementById('error1').style.display="block";
                cambia_clase('ins1','formLabel alert');
                return false;
            }
        }
        function validJob(){
            var job = document.getElementById('JobTitle').value;
            if(job != ""){
                return true;    
            }else{
                //Message
                //alert('Job Title required.');
                document.getElementById('error2').style.display="block";
                cambia_clase('ins2','formLabel alert');
                return false;
            }
        }
        function validSchool(){
            var sc = trim(document.getElementById('BldgName').value);
            document.getElementById('BldgName').value = sc;
            if(sc != ""){
                return true;    
            }else{
                //Message
                //alert('School/District required.');
                document.getElementById('error3').style.display="block";
                cambia_clase('ins3','formLabel alert');
                return false;
            }
        }
        function validConPurchFor(){
            var pur = document.getElementById('ConPurchFor').value;
            if(pur != ""){
                return true;    
            }else{
                //Message
                //alert('Purchase required.');
                document.getElementById('error4').style.display="block";
                cambia_clase('ins4','formLabel alert');
                return false;
            }
        }
        function validZip(){
            var zip = trim(document.getElementById('Zip').value);
            document.getElementById('Zip').value = zip;
            if(zip != ""){
                    if(!isNaN(zip)){
                        return true;
                    }else{
                        //alert('Zip/Postal code invalid.');
                        document.getElementById('error6').style.display="block";
                        document.getElementById('error6').innerHTML="<p><-Zipcode invalid</p>";
                        cambia_clase('ins6','formLabel alert');
                        return false;
                    }
            }else{
                 //alert('Zip/Postal code required.');
                 document.getElementById('error6').style.display="block";
                 document.getElementById('error6').innerHTML="<p><-Required</p>";
                 cambia_clase('ins6','formLabel alert');
                 return false;
            }
        }
        function validPhone(){
            var p = trim(document.getElementById('Phone').value);
            document.getElementById('Phone').value = p;
            if(p != ""){
                    /*if(p.match('^[0-9][-0-9]*[0-9]$')){*/
                        return true;
                    /*}else{
                        //alert('Phone number invalid.');
                        document.getElementById('error7').style.display="block";
                        document.getElementById('error7').innerHTML = "<p><-Phone Number Invalid</p>";
                        cambia_clase('ins7','formLabel alert');
                        return false;
                    }*/
            }else{
                 //alert('Phone number required.');
                 document.getElementById('error7').style.display="block";
                 document.getElementById('error7').innerHTML = "<p><-Required</p>";
                cambia_clase('ins7','formLabel alert');
                 return false;
            }
        }
        function validAddress(){
            var name = trim(document.getElementById('Address1').value);
            document.getElementById('Address1').value = name;
            if(name != ""){
                return true;    
            }else{
                //Message
                //alert('Address required.');
                document.getElementById('error5').style.display="block";
                cambia_clase('ins5','formLabel alert');
                return false;
            }
        }

        function validForm(){
            //cleanProducts();
            for(i = 1; i<=8;i++){
                cambia_clase('ins'+i.toString(),'formLabel');
                document.getElementById('error'+i.toString()).style.display = "none";
            }
            document.getElementById('errorGlobal').style.display="none";
            if(validName() && validJob() && validSchool() && validConPurchFor()&& validAddress() && validZip() && validPhone() && checkEmail()){
                return true;
            }else{
                document.getElementById('errorGlobal').style.display="block";
                return false;
            }
        }
        function checkEmail(){
            var pattern=/^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
            inputvalue = trim(document.getElementById('Email').value);
            document.getElementById('Email').value = inputvalue;
            if(pattern.test(inputvalue)){
		        return true;
            }else{   
		        //alert("Email invalid."); 
		        document.getElementById('error8').style.display="block";
                cambia_clase('ins8','formLabel alert');
		        return false;
            }
        }