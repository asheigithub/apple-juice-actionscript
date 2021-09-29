package com.adobe.flash.compiler.clients;

import java.io.BufferedReader;    
import java.io.IOException;    
import java.io.InputStream;    
import java.io.InputStreamReader;

public class MXMLC
{
	  public static void main(String args[])
	  {
		  System.out.println("will be compiled with ASTool compiler.");
		  
		  String rootdir="";
		  
		  for (int i=0; i < args.length; i++) {
			//System.out.println("      " + args[i]);
			
				if(args[i].startsWith("+flexlib="))
				{
					rootdir=args[i].substring(9,args[i].length()-10);
					
				}
			
			}
			
			String[] cmd=new String[args.length+1];
			cmd[0]=rootdir + "bin/CMXMLCCLI.exe";
			
			for (int i=0; i < args.length; i++) {
				//System.out.println("      " + args[i]);
				cmd[i+1]=args[i];
			}
			Process p;    
        
			try    
			 {    
				
				 p = Runtime.getRuntime().exec(cmd);    
				 
				 
				 { 
					InputStream instm=p.getInputStream();  
					InputStreamReader isr=new InputStreamReader(instm);    
					
					 BufferedReader br=new BufferedReader(isr);    
					 String line=null;    
					
					while((line=br.readLine())!=null)    
					 {    
						 System.out.println(line);    
					 }   
				}
				 
				 {
					 InputStream fis=p.getErrorStream();    
					
					 InputStreamReader isr=new InputStreamReader(fis);    
					
					 BufferedReader br=new BufferedReader(isr);    
					 String line=null;    
					
					while((line=br.readLine())!=null)    
					 {    
						 System.err.println(line);    
					 }   
				 }
				 
				 
				 
				int exitVal = p.waitFor();
				
				
				
				
				if(exitVal==0)
				{					
					
				}
				else
				{
					System.exit(-1);
				}
			 }    
			catch (Throwable e)    
			 {    
				 e.printStackTrace();    
			 }    

			
	  }
}