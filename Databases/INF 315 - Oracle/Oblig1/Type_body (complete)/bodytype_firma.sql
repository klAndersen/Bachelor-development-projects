create or replace
type body firma_type is overriding 
member function tostring return varchar2 
is
selger_info varchar2(2000);
begin
selger_info := ' Orginasjonsnr: ' || self.f_orgnr || ' Selgers id: ' || self.s_nr 
            || ' Selgers navn: ' || self.s_fornavn || ' ' || self.s_enavn;
if self.s_representerer is not null then --representerer selgeren noen? (eks:sameie)
  selger_info := selger_info || ' Representerer: ' || self.s_representerer;
end if;
return selger_info;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i firma_type.tostring()';
end tostring;
end;