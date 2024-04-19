﻿namespace Core.Request;

public class UpdateEnterpriseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<int>? PromotionsIdsToAdd { get; set; } = null;
    public List<int>? PromotionsIdsToRemove { get; set; } = null ;

}