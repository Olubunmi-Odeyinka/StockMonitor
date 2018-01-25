namespace StockExchange.Auth.Helpers
{
	public static class AuthConstants
	{

		public const string AuthenticationToken = "AuthToken";
	    public const string Sha512Key = "StockManager";
	    public const string Salt = "cRossOversm17a32";
	    public const string Vector = "stOckMonitor0987";
	    public const string Password = "14516166171717171";

        public const string AuthTokenExpired = "Your Authetication Token has expired";
        public const string AuthTokenInvalid = "Your Authetication Token is invalid";
        public const string NoAuthToken = "Your Request has no Authentication token";
	    public const string ServiceNoSupplied = "The Controller No Instantiate properly";
	}
}