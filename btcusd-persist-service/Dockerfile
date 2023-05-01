FROM mcr.microsoft.com/mssql/server:2019-latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Pwd12345
ENV MSSQL_PID=Developer
# Disable SSL encryption
ENV MSSQL_ENCRYPT=0
ENV MSSQL_SSL_CERT=0
EXPOSE 5001
CMD ["/opt/mssql/bin/sqlservr"]