<%@page import="dt.hin.android.kl_andersen.Databehandling"%>

<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Logger ut...</title>
</head>
<body>
	Du blir n� utlogget....
	<%
	Databehandling.avsluttSession();
%>
	<%@ include file="forwardinnlogging.jsp"%>
</body>
</html>