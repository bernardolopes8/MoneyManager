namespace MoneyManager
{
    class AccountParameters
    {
        public long account_id { get; set; }

        public AccountParameters(long id)
        {
            account_id = id;
        }
    }
}
